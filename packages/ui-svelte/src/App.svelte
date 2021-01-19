<script lang="ts">
	import Header from "./Components/Header.svelte";
	import { Router, Link, Route } from "svelte-navigator";
	import AuthCallback from "./Pages/AuthCallback.svelte";
	import Overview from "./Pages/Overview.svelte";
	import PrivateRoute from "./Components/PrivateRoute.svelte";
	import PlaylistDetail from "./Pages/PlaylistDetail.svelte";
	import Login from "./Components/Login.svelte";
</script>

<style>
	main {
		display: flex;
		justify-content: center;
		align-items: center;
		flex-flow: column wrap;
		max-width: 240px;
		margin: 0 auto;
		width: 1080px;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>

<Router>
	<main>
		<Header />
		<PrivateRoute path="/">
			<Overview />
		</PrivateRoute>
		<PrivateRoute path="playlists/:id" let:params>
			<PlaylistDetail params={params} />
		</PrivateRoute>
		<Route path="login" component={Login} />
		<Route path="auth/callback" component={AuthCallback} />
	</main>
</Router>
