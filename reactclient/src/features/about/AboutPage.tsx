import { Alert, AlertTitle, Button, ButtonGroup, List, ListItem, ListItemText, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { useState } from "react";
import agent from "../../app/api/agenct";

export default function AboutPage() {

    const [validationErrors, setValidationErrors] = useState<string[]>([]);

    function getValidationError() {
        agent.TestErrors.getValidationError()
            .then(() => console.log('should not see this'))
            .catch(error => setValidationErrors(error));
    }

    return (
        <Container>
            <Typography gutterBottom variant='h2'>ERRORS FOR TESTING PURPOSES</Typography>
            <ButtonGroup>
                <Button variant="contained" onClick={() => agent.TestErrors.get400Error().catch(error => console.log(error))}>Get 400 Error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get401Error().catch(error => console.log(error))}>Get 401 Error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get404Error().catch(error => console.log(error))}>Get 404 Error</Button>
                <Button variant="contained" onClick={() => agent.TestErrors.get500Error().catch(error => console.log(error))}>Get 500 Error</Button>
                <Button variant="contained" onClick={getValidationError}>
                    Get Validation Error
                </Button>
            </ButtonGroup>

            {validationErrors.length > 0 && 
                <Alert severity="error">
                    <AlertTitle>Validation Errors</AlertTitle>
                    <List>
                        {validationErrors.map(error => (
                            <ListItem key={error}>
                                <ListItemText>{error}</ListItemText>
                            </ListItem>
                        ))}
                    </List>
                </Alert>
            }

        </Container>
    )   
}