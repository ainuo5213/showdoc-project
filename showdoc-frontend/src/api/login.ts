import { ILoginForm } from "@/types/login";
import axios from "@/utils/http";

export async function login(form: ILoginForm) {
  return await axios.post("/api/auth/login", form);
}
