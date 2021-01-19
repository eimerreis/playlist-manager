import { writable } from "svelte/store";
import Spotify from "spotify-web-api-js";
import superagent from "superagent";

const createStore = () => {
	const tokenState = {
		accessToken: localStorage.getItem("accessToken") || "",
		refreshToken: localStorage.getItem("refreshToken") || "",
		user: {
			id: "",
			displayName: "",
		},
		initialized: false,
	};

	const getUser = async (accessToken) => {
		const client = new Spotify();
		client.setAccessToken(accessToken);
		const meResponse = await client.getMe();
		return meResponse;
	};

	const updateTokens = async (accessToken, refreshToken) => {
		localStorage.setItem("accessToken", accessToken);
		localStorage.setItem("refreshToken", refreshToken);

		const meResponse = await getUser(accessToken);

		update(() => ({
			accessToken,
			refreshToken,
			user: {
				displayName: meResponse.display_name,
				id: meResponse.id,
			},
		}));
	};

	const refreshAccessToken = async (refreshToken) => {
		const tokenResponse = await superagent
            .post("https://accounts.spotify.com/api/token")
            .type("form")
			.send({
				grant_type: "refresh_token",
				client_id: __app.env.SPOTIFY_CLIENT_ID,
				refresh_token: refreshToken,
			});
		return {
			...tokenResponse.body,
		};
	};

	// call get user with current token, to see if its still valid, otherwise refresh token
	const { accessToken, refreshToken } = tokenState;
	if (accessToken && refreshToken) {
		getUser(accessToken)
			.then((meResponse) => {
				update((state) => ({
					...state,
					initialized: true,
					user: {
						displayName: meResponse.display_name,
						id: meResponse.id,
					},
				}));
			})
			.catch((err) => {
				console.log(err);
				if (err.status === 401 && refreshToken) {
					refreshAccessToken(refreshToken).then((tokenResponse) => {
						updateTokens(
							tokenResponse.accessToken,
							tokenResponse.refreshToken
						);
					});
				}
			});
	}

	const { subscribe, update } = writable(tokenState);
	return {
		subscribe,
		updateTokens,
	};
};

export const AuthenticationStore = createStore();
