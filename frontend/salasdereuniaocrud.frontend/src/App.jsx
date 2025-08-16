import AppRoutes from './routes'
import { GlobalStyle } from './styles/global';
import { BrowserRouter } from 'react-router-dom';
import {AppContainer, PageWrapper} from './AppStyles.js'

function App() {
  return (
   <BrowserRouter>
   <GlobalStyle />
   <AppContainer>
    <PageWrapper>
      <AppRoutes />
    </PageWrapper>
   </AppContainer>
   </BrowserRouter>
  )
}

export default App
