<script lang="ts">
	import Header from "./Components/Header.svelte";
	import { Router, Link, Route } from "svelte-navigator";
	import AuthCallback from "./Pages/AuthCallback.svelte";
	import Overview from "./Pages/Overview.svelte";
	import PrivateRoute from "./Components/PrivateRoute.svelte";
	import PlaylistDetail from "./Pages/PlaylistDetail.svelte";
	import Login from "./Components/Login.svelte";
</script>

<style global lang="postcss">
	/* only apply purgecss on utilities, per Tailwind docs */
  	/* purgecss start ignore */
  	@tailwind base;
  	@tailwind components;
  	/* purgecss end ignore */

	@tailwind utilities;  
</style>

<Router>
	<main class="container mx-auto max-w-screen-lg">
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
