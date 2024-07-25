export async function login(props: { email: string; password: string }) {
  return await fetch(process.env["NEXT_PUBLIC_API_URL"] + "/auth/login", {
    headers: { "Content-Type": "application/json", accept: "*/*" },
    body: JSON.stringify(props),
    method: "POST",
    credentials: "include",
  });
}

export async function register(props: {
  email: string;
  username: string;
  password: string;
}) {
  return await fetch(process.env["NEXT_PUBLIC_API_URL"] + "/auth/register", {
    headers: { "Content-Type": "application/json", accept: "*/*" },
    body: JSON.stringify(props),
    method: "POST",
    credentials: "include",
  });
}
