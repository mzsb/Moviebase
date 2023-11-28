import { Guid } from "typescript-guid";
import { Genre } from "./genre";
import { Actor } from "./actor";

export type Movie = {
    movieId: Guid;
    title: string;
    year: string;
    posterId: string;
    imdbRating: number;
    genres: Genre[];
    actors: Actor[];
}