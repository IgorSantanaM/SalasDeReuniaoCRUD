import AppRoutes from './routes'
import { GlobalStyle } from './styles/global';
import { BrowserRouter } from 'react-router-dom';
import {AppContainer} from './AppStyles.js'

function App() {
  return (
   <BrowserRouter>
   <GlobalStyle />
   <AppContainer>
    <AppRoutes />
   </AppContainer>
   </BrowserRouter>
  )
}

export default App
