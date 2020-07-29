import logger from "loglevel";

import { AcquireToken } from "./Spotify/AcquireToken";

(async () => {
    logger.setLevel("INFO");
    
    AcquireToken();
})();
