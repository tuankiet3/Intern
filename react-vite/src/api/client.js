const DEFAULT_BASE_URL =
  import.meta.env.VITE_API_BASE_URL || "http://localhost:5096";

function buildUrl(path) {
  const trimmedBase = DEFAULT_BASE_URL.replace(/\/$/, "");
  const trimmedPath = path.startsWith("/") ? path : `/${path}`;
  return `${trimmedBase}${trimmedPath}`;
}

export async function fetchJson(path, options = {}) {
  const response = await fetch(buildUrl(path), {
    headers: {
      "Content-Type": "application/json",
      ...(options.headers || {}),
    },
    ...options,
  });
  if (!response.ok) {
    const text = await response.text().catch(() => "");
    throw new Error(text || `Request failed with status ${response.status}`);
  }
  const contentType = response.headers.get("content-type") || "";
  if (contentType.includes("application/json")) {
    return response.json();
  }
  return null;
}

export const api = {
  // Catalogs
  getProvinces: () => fetchJson("/api/Catalogs/provinces"),
  getHighSchoolsByProvince: (provinceId) =>
    fetchJson(`/api/Catalogs/highschool/${provinceId}`),
  getAreas: () => fetchJson("/api/Catalogs/areas"),
  getAspirations: () => fetchJson("/api/Catalogs/aspirations"),
  getMajors: () => fetchJson("/api/Catalogs/majors"),

  // Students
  registerStudent: (payload) =>
    fetchJson("/api/Student/register", {
      method: "POST",
      body: JSON.stringify(payload),
    }),
};
