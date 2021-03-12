import React from 'react'
import useMediaQuery from '@material-ui/core/useMediaQuery'
import { CssBaseline, AppBar, Toolbar, Typography } from '@material-ui/core'
import { createMuiTheme, ThemeProvider, makeStyles } from '@material-ui/core/styles'
import { Count } from './components'

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(2),
  },
}));

function App() {
  const classes = useStyles();
  const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)')
  const theme = React.useMemo(
    () =>
      createMuiTheme({
        props: {
          MuiTextField: {
            variant: "outlined"
          }
        },
        typography: {
          button: {
            textTransform: "none"
          }
        },
        palette: {
          type: prefersDarkMode ? 'dark' : 'light',
        },
      }),
    [prefersDarkMode],
  )
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <div className={classes.root}>
        <AppBar position="fixed">
          <Toolbar>
            <Typography variant="h6">Test</Typography>
          </Toolbar>
        </AppBar>
        <main className={classes.content}>
          <h5>Counter</h5>
          <Count />
        </main>
      </div>
    </ThemeProvider>
  );
}

export default App;
