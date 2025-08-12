import { useEffect, useMemo, useState } from "react";
import { api } from "../api/client";

const initialForm = {
  cccd: "",
  studentName: "",
  dateOfBirth: "",
  gender: true,
  phone: "",
  email: "",
  address: "",
  provinceId: "",
  highSchoolId: "",
  areaId: "",
  prioritize: false,
  majorId: "",
  aspirationId: "",
};

export default function RegisterForm() {
  const [form, setForm] = useState(initialForm);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState("");
  const [provinces, setProvinces] = useState([]);
  const [highSchools, setHighSchools] = useState([]);
  const [areas, setAreas] = useState([]);
  const [aspirations, setAspirations] = useState([]);
  const [majors, setMajors] = useState([]);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    if (message) {
      const timer = setTimeout(() => {
        setMessage("");
      }, 3000);
      return () => clearTimeout(timer);
    }
  }, [message]);
  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const [prov, area, asp, maj] = await Promise.all([
          api.getProvinces(),
          api.getAreas(),
          api.getAspirations(),
          api.getMajors(),
        ]);
        if (!cancelled) {
          setProvinces(prov);
          setAreas(area);
          setAspirations(asp);
          setMajors(maj);
          setLoading(false);
        }
      } catch (err) {
        if (!cancelled) setMessage(err.message || "Failed to load catalogs");
      }
    })();
    return () => {
      cancelled = true;
    };
  }, []);

  useEffect(() => {
    if (!form.provinceId) {
      setHighSchools([]);
      setForm((f) => ({ ...f, highSchoolId: "" }));
      return;
    }
    let cancelled = false;
    (async () => {
      try {
        const data = await api.getHighSchoolsByProvince(form.provinceId);
        if (!cancelled) setHighSchools(data);
      } catch (err) {
        if (!cancelled)
          setMessage(err.message || "Failed to load high schools");
      }
    })();
    return () => {
      cancelled = true;
    };
  }, [form.provinceId]);

  function onChange(e) {
    const { name, value, type, checked } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  }

  async function onSubmit(e) {
    e.preventDefault();
    setSubmitting(true);
    setMessage("");
    try {
      const payload = {
        Cccd: form.cccd.trim(),
        StudentName: form.studentName.trim(),
        Email: form.email.trim(),
        Phone: form.phone.trim(),
        DateOfBirth: new Date(form.dateOfBirth).toISOString(),
        Address: form.address.trim(),
        Prioritize: Boolean(form.prioritize),
        ProvinceId: Number(form.provinceId),
        HighSchoolId: Number(form.highSchoolId),
        AreaId: Number(form.areaId),
        MajorId: Number(form.majorId),
        AspirationId: form.aspirationId ? Number(form.aspirationId) : null,
        Gender: form.gender === true || form.gender === "true",
      };
      const res = await api.registerStudent(payload);
      setMessage(res?.message || "Registered successfully");
      setForm(initialForm);
    } catch (err) {
      setMessage(err.message || "Registration failed");
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <form
      onSubmit={onSubmit}
      className="max-w-6xl p-4 mx-auto space-y-6 bg-white"
    >
      {loading && (
        <div className="p-4 text-center text-gray-600">
          <div className="inline-block w-6 h-6 border-2 border-red-700 border-t-transparent rounded-full animate-spin mr-2"></div>
          Đang tải dữ liệu...
        </div>
      )}
      <div>
        <div className="px-3 py-2 font-bold text-white uppercase bg-red-700">
          Thông tin thí sinh
        </div>
        <p className="p-3 text-sm italic">
          Thí sinh vui lòng nhập số CCCD để tra cứu xem đã có thông tin đăng ký
          trước đây chưa. Nếu kết quả tìm không thấy, thí sinh vui lòng nhập vào
          các trường thông tin còn thiếu.
        </p>

        <div className="grid grid-cols-1 gap-4 p-3 md:grid-cols-2">
          {/* Hàng 1 */}
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Số CCCD <span className="text-red-500">*</span>
            </label>
            <input
              type="number"
              name="cccd"
              value={form.cccd}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              placeholder="Nhập Căn cước công dân"
              required
            />
          </div>
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Họ và tên <span className="text-red-500">*</span>
            </label>
            <input
              name="studentName"
              value={form.studentName}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              placeholder="Nhập Họ và tên"
              required
            />
          </div>

          {/* Hàng 2 */}
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Ngày sinh <span className="text-red-500">*</span>
            </label>
            <input
              name="dateOfBirth"
              value={form.dateOfBirth}
              onChange={onChange}
              type="date"
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              required
            />
          </div>
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Giới tính <span className="text-red-500">*</span>
            </label>
            <div className="flex items-center flex-1 gap-4">
              <label className="flex items-center gap-1">
                <input
                  type="radio"
                  name="gender"
                  value={true}
                  checked={form.gender === true}
                  onChange={onChange}
                />
                Nam
              </label>
              <label className="flex items-center gap-1">
                <input
                  type="radio"
                  name="gender"
                  value={false}
                  checked={form.gender === false}
                  onChange={onChange}
                />
                Nữ
              </label>
            </div>
          </div>

          {/* Hàng 3 */}
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Số điện thoại <span className="text-red-500">*</span>
            </label>
            <input
              type="tel"
              name="phone"
              value={form.phone}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              placeholder="Nhập số điện thoại"
              required
            />
          </div>
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Email <span className="text-red-500">*</span>
            </label>
            <input
              type="email"
              name="email"
              value={form.email}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              placeholder="Nhập địa chỉ email chính"
              required
            />
          </div>
          <div className="text-xs italic pl-44 text-gray-500 md:col-span-2">
            (Các thông báo quan trọng về kết quả, trường sẽ gửi cho thí sinh qua
            Zalo, SMS và Email)
          </div>
          {/* Hàng 4 */}
          <div className="flex flex-col md:col-span-2">
            <div className="flex items-center gap-4 md:col-span-2">
              <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
                Địa chỉ nhà <span className="text-red-500">*</span>
              </label>
              <input
                required
                name="address"
                value={form.address}
                onChange={onChange}
                className="flex-1 p-2 bg-gray-100 focus:outline-none"
                placeholder="Tổ/Đội, Thôn/ Xóm, Xã-Huyện..."
              />
            </div>
            <div className="mt-1 text-xs italic text-gray-600 pl-44">
              (Nhập đầy đủ Tổ/Đội, Thôn/ Xóm, Xã-Huyện- Tỉnh hoặc Số nhà, tên
              đường, Phường, Quận, Tỉnh/ Thành phố)
            </div>
          </div>

          {/* Hàng 5 */}
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Tỉnh/Thành phố <span className="text-red-500">*</span>
            </label>
            <select
              name="provinceId"
              value={form.provinceId}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              required
            >
              <option value="">Chọn Tỉnh/Thành phố</option>
              {provinces.map((p) => (
                <option key={p.provinceId} value={p.provinceId}>
                  {p.provinceName}
                </option>
              ))}
            </select>
          </div>
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Trường THPT/TGDTX <span className="text-red-500">*</span>
            </label>
            <select
              name="highSchoolId"
              value={form.highSchoolId}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
              required
              disabled={!form.provinceId}
              style={{
                backgroundColor: !form.provinceId ? "gray" : "",
              }}
            >
              <option value="">Chọn trường THPT</option>
              {highSchools.map((school) => (
                <option key={school.highSchoolId} value={school.highSchoolId}>
                  {school.highSchoolName}
                </option>
              ))}
            </select>
          </div>
          <div className="text-xs italic pl-44 text-gray-500 md:col-span-2">
            (Chọn Tỉnh/ Thành phố bạn đang sinh sống. Nếu là thí sinh tự do thì
            bạn chọn trường THPT/ TTGDTX đã tốt nghiệp trước đó.)
          </div>
          {/* Hàng 6 */}
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Khu vực <span className="text-red-500">*</span>
            </label>
            <select
              required
              name="areaId"
              value={form.areaId}
              onChange={onChange}
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
            >
              <option value="">-- Chọn Khu vực --</option>
              {areas.map((area) => (
                <option key={area.areaId} value={area.areaId}>
                  {area.areaName}
                </option>
              ))}
            </select>
          </div>
          <div className="flex items-center gap-4">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Đối tượng <span className="text-red-500">*</span>
            </label>
            <select
              required
              name="prioritize"
              value={form.prioritize ? "1" : "0"}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  prioritize: e.target.value === "1",
                }))
              }
              className="flex-1 p-2 bg-gray-100 focus:outline-none"
            >
              <option value="0">0 - Không ưu tiên</option>
              <option value="1">1 - Có ưu tiên</option>
            </select>
          </div>
        </div>
      </div>

      {/* THÔNG TIN ĐĂNG KÝ */}
      <div>
        <div className="px-3 py-2 font-bold text-white uppercase bg-red-700">
          Thông tin đăng ký
        </div>
        <div className="grid grid-cols-1 gap-4 p-3 md:grid-cols-2">
          <div className="flex flex-col md:col-span-2">
            <div className="flex items-center gap-4">
              <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
                Đăng ký ngành <span className="text-red-500">*</span>
              </label>
              <select
                name="majorId"
                value={form.majorId}
                onChange={onChange}
                className="w-xl p-2 bg-gray-100 focus:outline-none"
                required
              >
                <option value="">Chọn ngành</option>
                {majors.map((major) => (
                  <option key={major.majorId} value={major.majorId}>
                    {major.majorName}
                  </option>
                ))}
              </select>
            </div>
            <div className="mt-1 text-xs italic text-gray-600 pl-44">
              <span className="font-bold text-black">Ghi chú:</span>
              <br />
              Thí sinh được phép đổi ngành phù hợp sau 1 Học kỳ nếu ngành học
              chưa được Bộ Giáo dục và Đào tạo.
            </div>
          </div>

          <div className="flex items-center gap-4 md:col-span-2">
            <label className="w-40 text-sm font-medium text-left whitespace-nowrap">
              Có nguyện vọng du học
            </label>
            <div className="flex items-center flex-1 gap-4">
              <select
                name="aspirationId"
                value={form.aspirationId}
                onChange={onChange}
                className="w-xl p-2 bg-gray-100 focus:outline-none"
              >
                <option value="">Chọn nguyện vọng du học</option>
                {aspirations.map((aspiration) => (
                  <option
                    key={aspiration.aspirationId}
                    value={aspiration.aspirationId}
                  >
                    {aspiration.aspirationName}
                  </option>
                ))}
              </select>
              <p className="text-xs italic text-gray-500">
                Nếu có nguyện vọng du học thì chọn hình thức du học.
              </p>
            </div>
          </div>
        </div>
      </div>

      {/* Message display */}

      {message && (
        <div
          className={`p-3 mx-3 rounded ${
            message.includes("successfully") || message.includes("success")
              ? "bg-green-100 text-green-800 border border-green-300"
              : "bg-red-100 text-red-800 border border-red-300"
          }`}
        >
          {message}
        </div>
      )}

      {/* Nút submit */}
      <div className="flex justify-end">
        <button
          type="submit"
          className="px-6 py-2 text-white bg-red-700 rounded hover:bg-red-800 disabled:opacity-50 disabled:cursor-not-allowed"
          disabled={submitting}
        >
          {submitting ? "Đang gửi..." : "Gửi đăng ký"}
        </button>
      </div>
    </form>
  );
}
