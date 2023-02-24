import Catalog from "../../features/catalog/Catalog";
import Header from "./Header";
import { Container, createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ProductDetails from "../../features/catalog/ProductDetails";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css'
import BasketPage from "../../features/basket/BasketPage";
import LoadingComponent from "./LoadingComponent";
import CheckoutPage from "../../features/checkout/CheckoutPage";
import { useAppDispatch } from "../store/configureStore";
import { fetchBasketAsync } from "../../features/basket/basketSlice";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import { fetchCurrentUserAsnyc } from "../../features/account/accountSlice";

function App() {

  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  const initApp = useCallback(async () => {
    try{
      await dispatch(fetchCurrentUserAsnyc())
      await dispatch(fetchBasketAsync())
    }
    catch(error)
    {
      console.log('error');
    }
  }, [dispatch]);

  useEffect(() => {
    initApp().then(() => setLoading(false))
  }, [initApp])

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
              <Route path='/checkout' element={<CheckoutPage />} />
              <Route path='/login' element={<Login />} />
              <Route path='/register' element={<Register />} />
          </Routes>
        </Container>
    </ThemeProvider>
  );
}

export default App;