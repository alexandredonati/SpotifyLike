import { Song } from "./song";

export interface Album{
    id: string,
    artistIds: string[],
    nome: string,
    musicas: Song[]
}