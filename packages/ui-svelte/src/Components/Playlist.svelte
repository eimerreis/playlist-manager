<script lang="typescript">
	import type { Playlist } from "../Types/Playlist";
	import { fade } from "svelte/transition";
	import { Link } from "svelte-navigator";

	export let playlist: Playlist;
	const image = playlist.images[0].url;
</script>

<Link to="/playlists/{playlist.id}">
	<div
		transition:fade={{ duration: 200 }}
		class="max-w-md mx-auto bg-white rounded-xl shadow-md overflow-hidden md:max-w-2xl"
	>
		<div class="md:flex flex-col">
			<div class="md:flex-shrink-0">
				<img
					class="h-32 w-full object-cover"
					src={image}
					alt={playlist.name}
				/>
			</div>
			<div class="p-8">
				<span
					class="block mt-1 text-2xl text-center leading-tight font-medium text-black hover:underline"
					>{playlist.name}</span
				>
			</div>
		</div>
	</div>
</Link>

<!-- <div  class="playlist-card">
    <Link to="/playlists/{playlist.id}">
        <h1>{playlist.name}</h1>
        <img src={image} alt={playlist.name} />
    
</div> -->
