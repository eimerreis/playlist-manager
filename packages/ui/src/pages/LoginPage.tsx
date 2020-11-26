import React from "react";
import { RouteComponentProps } from "@reach/router";
import { Flex, Button, Container } from "theme-ui";
import styled from "styled-components";


export const LoginPage: React.FC<RouteComponentProps> = () => {
    return ( 
        <Container>
            <Button variant="primary">Login with Spotify</Button>
        </Container>
    )
}