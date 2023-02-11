import { Add, Delete, Remove } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useState } from "react";
import agent from "../../app/api/agenct";
import { useStoreContext } from "../../app/context/SoreContext";
import BasketSummary from "./BasketSummary";

function BasketPage() {

    const {basket, setBasket, removeItem} = useStoreContext();
    const [status, setStatus] = useState({
        loading: false,
        name: '',
    });

    function handleAddItem(productKey: string, name: string) {
        setStatus({loading: true, name});
        agent.Basket.addItem(productKey)
            .then(basket => setBasket(basket))
            .catch(error => console.log(error))
            .finally(() => setStatus({loading: false, name: ''}));
    }

    function handleRemoveItem(productKey: string, quantity = 1, name: string) {
        setStatus({loading: true, name});
        agent.Basket.removeItem(productKey, quantity)
            .then(() => removeItem(productKey, quantity))
            .catch(error => console.log(error))
            .finally(() => setStatus({loading: false, name: ''}));
    }

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
                                        loading={status.loading && status.name === 'rem'+basketItem.productKey}
                                        onClick={() => handleRemoveItem(basketItem.productKey, 1, 'rem'+basketItem.productKey)}>
                                        <Remove />
                                    </LoadingButton>
                                    {basketItem.quantity}
                                    <LoadingButton
                                        loading={status.loading && status.name === 'add'+basketItem.productKey}
                                        onClick={() => handleAddItem(basketItem.productKey, 'add'+basketItem.productKey)}
                                        color="secondary">
                                        <Add />
                                    </LoadingButton>
                                </TableCell>
                                <TableCell align="right">${basketItem.quantity * basketItem.price}</TableCell>
                                <TableCell align="right">
                                    <LoadingButton
                                        loading={status.loading && status.name === 'del'+basketItem.productKey}
                                        onClick={() => handleRemoveItem(basketItem.productKey, basketItem.quantity, 'del'+basketItem.productKey)} color="error">
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
                </Grid>
            </Grid>
        </>
    )
}

export default BasketPage;