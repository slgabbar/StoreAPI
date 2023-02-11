import Catalog from "../../features/catalog/Catalog";
import Header from "./Header";
import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useEffect, useState } from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ProductDetails from "../../features/catalog/ProductDetails";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css'
import BasketPage from "../../features/basket/BasketPage";
import { useStoreContext } from "../context/SoreContext";
import { getCookie } from "../util/util";
import agent from "../api/agenct";
import LoadingComponent from "./LoadingComponent";

function App() {
  const {setBasket} = useStoreContext(); 
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const userKey = getCookie('userKey');
    if (userKey) {
      agent.Basket.get()
        .then(basket => setBasket(basket))
        .catch(error => console.log(error))
        .finally(() => setLoading(false));
    } else {
      setLoading(false);  
    }
  }, [setBasket])

  const [darkMode, setDarkMode] = useState(false);
  const paletteType = darkMode ? 'dark' : 'light';

  const theme = createTheme({
    palette: {
      mode: paletteType,
      background: {
        default: (paletteType === 'light' ? '#eaeaea' : '#121212')
      }
    },
  })

  function handleThemeChange() {
    setDarkMode(!darkMode);
  }

  if (loading) return <LoadingComponent message="loading app..."/>

  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" hideProgressBar />
        <CssBaseline />
        <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
        <Container>
          <Routes>
              <Route path='/'  element={<HomePage />} />
              <Route  path='/catalog' element={<Catalog />} />
              <Route path='/catalog/:id' element={<ProductDetails />} />
              <Route path='/basket' element={<BasketPage />} />
          </Routes>
        </Container>
    </ThemeProvider>
  );
}

export default App;