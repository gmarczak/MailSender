export interface EmailDto {
  to: string;
  subject: string;
  body: string;
}

export interface SendMailResponse {
  appId: string;
  appName: string;
  status: string;
  email: EmailDto;
}
