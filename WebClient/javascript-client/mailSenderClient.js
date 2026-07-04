class MailSenderClient {
  constructor(baseUrl = "http://localhost:5134") {
    this.baseUrl = baseUrl;
  }

  async registerClientApp(appId, appName, pass) {
    const response = await fetch(`${this.baseUrl}/client-app/register`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ appId, appName, pass })
    });

    return await response.json();
  }

  async sendMail(token, to, subject, body) {
    const response = await fetch(`${this.baseUrl}/mail/send`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      },
      body: JSON.stringify({ to, subject, body })
    });

    return await response.json();
  }
}
