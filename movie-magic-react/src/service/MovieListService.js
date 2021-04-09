import { getData, postData } from "./api"

export function getMovieList(onSuccess, onFailure) {
    const config = { "content-type": "application/json" };
    getData(config, `http://localhost:5000/api/Movie/AllMovies`, onSuccess, onFailure);
}

export function getMovieDetailsById(id, source, onSuccess, onFailure) {
    const config = { "content-type": "application/json" };
    getData(config, `http://localhost:5000/api/Movie/movie?id=${id}&source=${source}`, onSuccess, onFailure);
}