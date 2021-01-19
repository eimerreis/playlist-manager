<script lang="ts">
    export let Label: string;
    export let Value: string;
</script>

<style>
    .setting-card {
        background: #fff;
        box-shadow: 1px 1px 4px -2px rgba(0, 0, 0, 0.8);
        border-radius: 5px;
        padding: 12px;

        display: grid;
        grid-template-columns: auto auto;
    }

    .setting-card .label {
        justify-self: center;
        font-weight: 300;
        font-size: 1.1rem;
        text-transform: uppercase;
        grid-column: 1 / 2;
        align-self: center;
    }

    .setting-card .number {
        font-size: 4rem;
        justify-self: center;
        font-weight: 200;
    }
</style>

<div class="setting-card">
    <span class="label">{Label}</span>
    <span class="number">{Value}</span>
</div>