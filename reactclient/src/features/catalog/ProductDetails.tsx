import { Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import agent from "../../app/api/agenct";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Product } from "../../app/models/products";

export default function ProductDetails() {
    const {id} = useParams<{id: string}>();
    const [product, setProduct] = useState<Product | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent.Catalog.details(id!)
            .then(product => setProduct(product))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [id])

    if (loading) return<LoadingComponent message="Loading products details..."/>

    if (product == null) return <span>Not Found</span>

    return (
        <Grid container spacing={6}>
            <Grid item xs={6}>
                <img src={product.pictureUrl} alt={product.name} style={{width: '100%'}}></img>
            </Grid>
            <Grid item xs={6}>
                <Typography variant="h3">{product.name}</Typography>
                <Divider sx={{mb: 2}}/>
                <Typography variant="h4" color='secondary'>${product.price.toFixed(2)}</Typography>
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>{product.name}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Description</TableCell>
                                <TableCell>{product.description}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Type</TableCell>
                                <TableCell>{product.type}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Quantity in stock</TableCell>
                                <TableCell>{product.quantityInStock}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>
    )   
}