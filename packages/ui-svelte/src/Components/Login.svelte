<script>
    const SpotifyClientId = "78f14bd06bcc43668e12a1c9246e3c72";
    const Scopes = "playlist-modify-public playlist-modify-private playlist-read-private";
    const RedirectUri = "http://localhost:5000/auth/callback";
</script>
<style>
    a {
        border: 1px solid;
    }
</style>

<p>Please Login with your Spotify Account to use Playlist Manager</p>
<a href="https://accounts.spotify.com/authorize?response_type=code&client_id={SpotifyClientId}&scope={encodeURIComponent(Scopes)}&redirect_uri={encodeURIComponent(RedirectUri)}">Login</a>