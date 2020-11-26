import React from 'react';
import logo from './logo.svg';
import { ThemeProvider } from "theme-ui";
import { base } from "@theme-ui/presets";
import { LoginPage } from "./pages/LoginPage";


function App() {
  return (
    <ThemeProvider theme={base}>
      <LoginPage path="/login" default />
    </ThemeProvider>
  );
}

export default App;
