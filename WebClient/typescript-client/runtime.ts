export class ApiError extends Error {
  constructor(public status: number, public body: string) {
    super(`API error ${status}: ${body}`);
  }
}

export async function requestJson<T>(url: string, options: RequestInit): Promise<T> {
  const response = await fetch(url, options);
  const text = await response.text();

  if (!response.ok) {
    throw new ApiError(response.status, text);
  }

  return text ? JSON.parse(text) as T : undefined as T;
}
