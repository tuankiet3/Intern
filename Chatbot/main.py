
import os
import pandas as pd
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from sqlalchemy import create_engine
from langchain.schema import Document
from langchain_huggingface import HuggingFaceEmbeddings
from langchain_community.vectorstores import FAISS
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain_google_genai import ChatGoogleGenerativeAI
from langchain.chains import RetrievalQA
from langchain.prompts import PromptTemplate
from dotenv import load_dotenv

load_dotenv()

app = FastAPI(title="Simple Chat API")

class ChatRequest(BaseModel):
    message: str

class ChatResponse(BaseModel):
    response: str

qa_chain = None

def setup_chatbot():
    global qa_chain
    try:
        # Database connection
        engine = create_engine(
            "mssql+pyodbc://@TUANKIET\\MSSQLSERVER01/ChatbotDB"
            "?driver=ODBC+Driver+17+for+SQL+Server&trusted_connection=yes"
        )
        
        # Load data
        df = pd.read_sql("SELECT ProductName, Description, Price FROM Products", engine)
        
        # Create documents
        docs = [
            Document(
                page_content=f"Tên sản phẩm: {row['ProductName']}. Mô tả: {row['Description']}. Giá: {row['Price']}",
                metadata={"ProductName": row["ProductName"], "Price": row["Price"]}
            )
            for _, row in df.iterrows()
        ]
        # embedding and text splitting
        embeddings = HuggingFaceEmbeddings(model_name="sentence-transformers/all-MiniLM-L6-v2")
        text_splitter = RecursiveCharacterTextSplitter(chunk_size=500, chunk_overlap=50)
        texts = text_splitter.split_documents(docs)
  
        # create vector store
        vectorstore = FAISS.from_documents(texts, embeddings)
        # LLM
        os.environ["GOOGLE_API_KEY"] = os.getenv("GOOGLE_API_KEY")
        llm = ChatGoogleGenerativeAI(model="gemini-2.5-flash", temperature=0.7)
        
        # Create prompt
        prompt_template = """
        Bạn là một nhân viên tư vấn bán hàng thân thiện và chuyên nghiệp.
        Nhiệm vụ của bạn là hỗ trợ khách hàng trả lời các câu hỏi dựa trên thông tin sản phẩm được cung cấp dưới đây.
        
        QUAN TRỌNG: 
        - Chỉ sử dụng thông tin từ các sản phẩm được cung cấp trong phần context
        - Nếu khách hàng hỏi về một tên sản phẩm hoặc hãng nào đó chi tiết,hãy lọc theo tên sản phẩm hoặc hãng người dùng cần
        - Nếu khách hàng hỏi về giá trên một mức nào đó, hãy lọc chính xác theo giá
        - Luôn trả lời bằng giọng văn tự nhiên, gần gũi
        - Không bịa đặt thông tin không có trong dữ liệu

        Dưới đây là thông tin các sản phẩm liên quan đến câu hỏi của khách:
        ---
        {context}
        ---

        Câu hỏi của khách hàng: {question}

        Hãy phân tích kỹ các sản phẩm trong context và trả lời chính xác theo yêu cầu:
        """
        
        prompt = PromptTemplate(template=prompt_template, input_variables=["context", "question"])
        # Create QA chain
        qa_chain = RetrievalQA.from_chain_type(
            llm=llm,
            chain_type="stuff",
            retriever=vectorstore.as_retriever(search_type="mmr"),
            return_source_documents=False,
            chain_type_kwargs={"prompt": prompt}
        )

        return True
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))
        return False

@app.on_event("startup")
async def startup():
    setup_chatbot()

@app.post("/chat", response_model=ChatResponse)
async def chat(request: ChatRequest):
    if not qa_chain:
        raise HTTPException(status_code=503, detail="Chatbot not ready")
    try:
        response = qa_chain.invoke({"query": request.message})
        return ChatResponse(response=response["result"])
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
