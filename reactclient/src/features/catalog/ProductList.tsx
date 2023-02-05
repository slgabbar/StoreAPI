import { Grid } from "@mui/material";
import { Product } from "../../app/models/products";
import ProductCard from "./ProductCard";

interface Props {
    products: Product[];
}

function ProductList({products}: Props) {
    return (
        <>
            <Grid container spacing={3}>
                {products.map(product => (
                    <Grid key={product.productId} item xs={4}>
                        <ProductCard  product={product}/>
                    </Grid>
                ))}
            </Grid>
        </>
    )
}

export default ProductList;