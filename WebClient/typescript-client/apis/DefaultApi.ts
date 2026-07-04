import { requestJson } from "../runtime";
import { RegisterClientAppRequest } from "../models/RegisterClientAppRequest";
import { RegisterClientAppResponse } from "../models/RegisterClientAppResponse";
import { SendMailRequest } from "../models/SendMailRequest";
import { SendMailResponse } from "../models/SendMailResponse";

export class DefaultApi {
  constructor(private baseUrl: string = "http://localhost:5134") {}

  registerClientApp(request: RegisterClientAppRequest): Promise<RegisterClientAppResponse> {
    return requestJson<RegisterClientAppResponse>(`${this.baseUrl}/client-app/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(request)
    });
  }

  sendMail(token: string, request: SendMailRequest): Promise<SendMailResponse> {
    return requestJson<SendMailResponse>(`${this.baseUrl}/mail/send`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      },
      body: JSON.stringify(request)
    });
  }
}
