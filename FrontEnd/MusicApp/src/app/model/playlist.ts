import { Song } from "./song";

export interface Playlist {
    id: string,
    titulo: string,
    musicas: Song[],
    isPublic: boolean,
    proprietarioId: string
    dataCriacao: string
}