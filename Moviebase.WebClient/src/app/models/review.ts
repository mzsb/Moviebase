import { Guid } from "typescript-guid"

export type Review = {
    reviewId: Guid
    userId: Guid
    username: string
    movieId: Guid
    content: string
    creationDate: Date
    lastUpdationDate: Date
}