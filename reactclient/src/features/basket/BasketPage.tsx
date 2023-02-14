import { Add, Delete, Remove } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { Button, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync, removeBasketItemAsync } from "./basketSlice";
import BasketSummary from "./BasketSummary";

function BasketPage() {

    const {basket, status} = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();

    if (!basket) return <Typography variant="h3">Your basket is empty</Typography>

    return (
        <>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }}>
                    <TableHead>
                        <TableRow>
                            <TableCell>Product</TableCell>
                            <TableCell align="right">Price</TableCell>
                            <TableCell align="center">Quantity</TableCell>
                            <TableCell align="right">Subtotal</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {basket.items.map((basketItem) => (
                            <TableRow
                                key={basketItem.name}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {basketItem.name}
                                </TableCell>
                                <TableCell align="right">${basketItem.price}</TableCell>
                                <TableCell align="center">
                                    <LoadingButton
                                        loading={status === 'pendingRemoveItem'+basketItem.productKey+'rem'}
                                        onClick={() => dispatch(removeBasketItemAsync({
                                                productKey: basketItem.productKey,
                                                quantity: 1,
                                                name: 'rem'
                                            }))}>
                                        <Remove />
                                    </LoadingButton>
                                    {basketItem.quantity}
                                    <LoadingButton
                                        loading={status === 'pendingAddItem'+basketItem.productKey}
                                        onClick={() => dispatch(addBasketItemAsync({productKey: basketItem.productKey }))}
                                        color="secondary">
                                        <Add />
                                    </LoadingButton>
                                </TableCell>
                                <TableCell align="right">${basketItem.quantity * basketItem.price}</TableCell>
                                <TableCell align="right">
                                    <LoadingButton
                                        loading={status === 'pendingRemoveItem'+basketItem.productKey+'del'}
                                        onClick={() => dispatch(removeBasketItemAsync({
                                            productKey: basketItem.productKey, quantity: basketItem.quantity, name: 'del'
                                        }))}
                                        color='error'>
                                        <Delete/>
                                    </LoadingButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Grid container>
                <Grid item xs={6} />
                <Grid item xs={6}>
                    <BasketSummary/>
                    <Button
                        component={Link}
                        to='/checkout'
                        variant='contained'
                        size='large'
                        fullWidth>
                        Checkout
                    </Button>
                </Grid>
            </Grid>
        </>
    )
}

export default BasketPage;


