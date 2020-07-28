import SpotifyWebApi from "spotify-web-api-node";

interface Response<T> {
    body: T;
    headers: Record<string, string>;
    statusCode: number;
}

export const FetchAllItems = async <T>(callFunction: (limit: number, offset: number) => Promise<Response<SpotifyApi.PagingObject<T>>>, overrides?: { limit?: number } ) => {
    let records: T[] = [];
    let keepGoing = true;
    let offset = 0;
    let limit = overrides?.limit || 100;
    while (keepGoing) {
        const response = await callFunction(limit, offset);
        records = [...records, ...response.body.items];
        offset += limit;

        if (response.body.next === null) {
            keepGoing = false;
            return records;
        }
    }
    return records;
}
