<script>
	function generateCodeVerifier() {
		const code_verifier = generateRandomString(128);
		return code_verifier;
	}

	function generateRandomString(length) {
		var text = "";
		var possible =
			"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
		for (var i = 0; i < length; i++) {
			text += possible.charAt(
				Math.floor(Math.random() * possible.length)
			);
		}
		return text;
	}

	function generateCodeChallenge(code_verifier) {
		return base64URL(CryptoJS.SHA256(code_verifier));
	}
	function base64URL(string) {
		return string
			.toString(CryptoJS.enc.Base64)
			.replace(/=/g, "")
			.replace(/\+/g, "-")
			.replace(/\//g, "_");
	}

	let Verifier;
	let CodeChallenge;

	const SpotifyClientId = "78f14bd06bcc43668e12a1c9246e3c72";
	const Scopes =
		"playlist-modify-public playlist-modify-private playlist-read-private";
	const RedirectUri = "http://localhost:5000/auth/callback";

	const OnLoad = () => {
		localStorage.removeItem("verifier");
		Verifier = generateCodeVerifier();
		localStorage.setItem("verifier", Verifier);
		CodeChallenge = generateCodeChallenge(Verifier);
	};
</script>

<svelte:head
	><script
		on:load={OnLoad}
		src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/3.1.9-1/crypto-js.min.js">
	</script></svelte:head
>

<div class="flex flex-col">
	<p class="text-center font-sans p-12 text-2xl">
		Please Login with your Spotify Account to use Playlist Manager
	</p>
	<a
		class="mx-auto bg-green-300 text-center w-32 text-lg text-white p-6 rounded font-bold flex-initial"
		href="https://accounts.spotify.com/authorize?response_type=code&code_challenge_method=S256&code_challenge={CodeChallenge}&client_id={SpotifyClientId}&scope={encodeURIComponent(
			Scopes)}&redirect_uri={encodeURIComponent(RedirectUri)}">Login</a
	>
</div>
