<script>
    import { AuthenticationStore } from "../Stores/Authentication";
    import { ConfigurationStore } from "../Stores/Config";
    import querystring from "query-string";
    import superagent from "superagent";
    import { useNavigate, useLocation, Link } from "svelte-navigator";
    const navigate = useNavigate();
    const location = useLocation();

    async function ExchangeCodeforToken(code, verifier) {
        const url = "https://accounts.spotify.com/api/token";
        const response = await superagent.post(url).type("form").send({
            client_id: $ConfigurationStore.Spotify.ClientId,
            grant_type: "authorization_code",
            code,
            redirect_uri: $ConfigurationStore.Spotify.RedirectUri,
            code_verifier: verifier,
        });
        await AuthenticationStore.updateTokens(
            response.body.access_token,
            response.body.refresh_token
        );
        navigate("/", {
            state: { from: $location.pathname },
            replace: true,
        });
    }

    const queryParams = querystring.parse(window.location.search);
    const { code } = queryParams;
    const Verifier = localStorage.getItem("verifier");

    if (!code || !Verifier) {
        throw new Error("either Code or Verifier are undefined");
    }

    ExchangeCodeforToken(code, Verifier);
</script>

<p>You will be redirect to the app in a few seconds...</p>
<p>
    <Link to="/">Redirect did not work? Click here</Link>
</p>
