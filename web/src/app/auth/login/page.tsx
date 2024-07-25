"use client";
import { login } from "@/lib/actions";
import { useState } from "react";

export default function AuthPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    login({ email, password }).then((res) => {
      if ([401, 404].includes(res.status)) {
        setError("Invalid credentials");
        return;
      }
      if (res.status / 100 === 5) {
        setError("Server error");
        return;
      }
      setError("");
    });
  };

  return (
    <main>
      <h1>Register</h1>
      <form className="flex flex-col gap-2 w-32" onSubmit={handleSubmit}>
        <label htmlFor="email-field">Email:</label>
        <input
          id="email-field"
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <label htmlFor="password-field">Password:</label>
        <input
          id="password-field"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button>Login</button>
        {error.length > 0 && <p className="text-red-500">{error}</p>}
      </form>
    </main>
  );
}
