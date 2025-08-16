import AppRoutes from './routes'
import { GlobalStyle } from './styles/global';
import { BrowserRouter } from 'react-router-dom';
import {AppContainer, PageWrapper} from './AppStyles.js'
import { ToastContainer } from 'react-toastify';

function App() {
  return (
   <BrowserRouter>
   <GlobalStyle />
   <ToastContainer position="bottom-right" autoClose={5000}/>
    <AppContainer>
      <PageWrapper>
        <AppRoutes />
      </PageWrapper>
    </AppContainer>
   </BrowserRouter>
  )
}

export default App
