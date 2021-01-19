<script>
    import { AuthenticationStore } from "../Stores/Authentication";
    import { useNavigate, useLocation } from "svelte-navigator";
  
    const navigate = useNavigate();
    const location = useLocation();
  
    $: if (!$AuthenticationStore.accessToken) {
      navigate("/login", {
        state: { from: $location.pathname },
        replace: true,
      });
    }
  </script>
  
  {#if $AuthenticationStore.accessToken && $AuthenticationStore.initialized}
    <slot />
  {/if}
  