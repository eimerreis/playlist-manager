<script>
	import Header from "./Components/Header.svelte";
	import Login from "./Components/Login.svelte";
	import { Router, Link, Route } from "svelte-routing";
	import AuthCallback from "./Pages/AuthCallback.svelte";
</script>

<style>
	main {
		display: flex;
		justify-content: center;
		align-items: center;
		flex-flow: column wrap;
		max-width: 240px;
		margin: 0 auto;
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
		<Route path="/" component={Login} />
		<Route path="/auth/callback" component={AuthCallback} />
	</main>
</Router>
