async function sendMail() {
  const token = document.getElementById("token").value.trim();
  const to = document.getElementById("to").value.trim();
  const subject = document.getElementById("subject").value;
  const body = document.getElementById("body").value;
  const resultBox = document.getElementById("result");

  if (!token) {
    resultBox.textContent = "Brak tokena JWT. Najpierw zarejestruj aplikację w Swaggerze.";
    return;
  }

  try {
    const response = await fetch("http://localhost:5134/mail/send", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + token
      },
      body: JSON.stringify({ to, subject, body })
    });

    const text = await response.text();
    let output;

    try {
      output = JSON.stringify(JSON.parse(text), null, 2);
    } catch {
      output = text || "Brak treści odpowiedzi.";
    }

    resultBox.textContent = `HTTP ${response.status}\n\n${output}`;
  } catch (error) {
    resultBox.textContent = "Błąd połączenia z API: " + error.message;
  }
}
