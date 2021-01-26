<script lang="typescript">
	import { AuthenticationStore } from "../Stores/Authentication";
	import type { Playlist } from "../Types/Playlist";
	import Spotify from "spotify-web-api-js";
	import SettingCard from "../Components/SettingCard.svelte";

	export let params;
	let image = "";
	let tracks: SpotifyApi.TrackObjectFull[] = [];

	let playlistPromise: Promise<SpotifyApi.SinglePlaylistResponse>;
	if (params.id) {
		const client = new Spotify();
		client.setAccessToken($AuthenticationStore.accessToken);
		playlistPromise = client.getPlaylist(params.id);
		playlistPromise.then(async (x) => {
			let widestImage = 0;
			let url = "";
			for (let img of x.images) {
				if (img.width > widestImage) {
					widestImage = img.width;
					url = img.url;
				}
			}
			tracks = x.tracks.items.map(
				(x) => x.track as SpotifyApi.TrackObjectFull
			);
			image = url;
		});
	}
</script>

{#await playlistPromise then playlist}
	<section class="playlist-detail">
		<div class="grid grid-cols-5 pb-12">
			<div class="w-full  col-start-1 col-end-3">
				<img
					class="w-80 object-cover h-80 shadow-md rounded"
					src={playlist.images[0].url}
					alt={playlist.name}
				/>
			</div>
			<div class="col-start-3 col-span-3">
				<h3
					class="text-left text-3xl font-light tracking-wider uppercase pb-6"
				>{playlist.name}</h3>
				<div class="grid gap-4 grid-cols-2">
					<div class="rounded cursor-pointer bg-white shadow-md p-4 w-full flex flex-row hover:shadow-lg">
						<i class="fas fa-heart-rate text-5xl text-green-400 w-20 pr-6" />
						<div class="flex flex-col font-bold text-3xl">
							<span>Running</span>
							<span class="font-normal text-gray-300 text-sm uppercase">Job Status</span>
						</div>
					</div>
					<div class="rounded cursor-pointer bg-white shadow-md p-4 w-full flex flex-row hover:shadow-lg">
						<i class="fad fa-angle-up text-5xl text-indigo-700 w-14 pl-2" />
						<div class="flex flex-col font-bold text-3xl">
							<span>Descending</span>
							<span class="font-normal text-gray-300 text-sm uppercase">Sort Direction</span>
						</div>
					</div>
					<div class="rounded cursor-pointer bg-white shadow-md p-4 w-full flex flex-row hover:shadow-lg">
						<i class="fad fa-flag text-5xl text-yellow-500 w-20 pl-2" />
						<div class="flex flex-col font-bold text-3xl">
							<span>60</span>
							<span class="font-normal text-gray-300 text-sm uppercase">Maximum Tracks</span>
						</div>
                    </div>
                    <div class="rounded cursor-pointer bg-white shadow-md p-4 w-full flex flex-row hover:shadow-lg">
						<i class="fad fa-archive text-4xl text-center pt-2 pr-4 text-pink-400 w-14"></i>
						<div class="flex flex-col font-bold text-3xl">
							<span>Activated</span>
							<span
								class="font-normal text-gray-300 text-sm uppercase"
								>Archive List</span>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="border-t-4 border-gray-200">
			<div class="grid grid-cols-6 text-lg">
				<table class="w-full divide-y divide-gray-200">
					<thead class="bg-gray-50">
						<tr>
							<th
								scope="col"
								class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
								Name
							</th>
							<th
								scope="col"
								class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
								Artist
							</th>
						</tr>
					</thead>
					<tbody class="bg-white divide-y divide-gray-200">
						{#each tracks as track}
                            <tr>
                                <td class="px-6 py-4 whitespace-nowrap">
                                    {track.name}
                                </td>
                                <td class="px-6 py-4 whitespace-nowrap">
                                    {track.artists.map((x) => ` ${x.name}`)}
                                </td>
                                <td class="px-6 py-4 whitespace-nowrap">
                                    
                                </td>
                                <td
                                    class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    
                                </td>
                                <td
                                    class="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                                    <a
                                        href="#"
                                        class="text-indigo-600 hover:text-indigo-900"
                                        >Edit</a
                                    >
                                </td>
                            </tr>
						{/each}
						<!-- More items... -->
					</tbody>
				</table>
			</div>
		</div>
		<!-- <h2>{playlist.name}</h2>
		<SettingCard Label="Maximum Tracks" Value="60" />
		<SettingCard Label="Sorted" Value="New First" />
		<SettingCard Label="Archive List" Value="Yes" />
		<div class="status-card">
			<span class="label">Status</span>
			<span class="number">Running</span>
		</div> -->
	</section>
{/await}
