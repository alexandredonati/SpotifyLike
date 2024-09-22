import { Cartao } from "./cartao";

export interface User {
    id: string,
    name: string,
    email: string,
    dataNascimento: string,
    senha: string,
    planoId: string,
    cartao: Cartao
}

