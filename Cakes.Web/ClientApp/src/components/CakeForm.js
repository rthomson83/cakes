import React, {useState} from 'react';
import useAxios from "axios-hooks";
import {Button, Form, FormFeedback, FormGroup, Input, Label} from "reactstrap";
import {useHistory} from "react-router-dom";

export function CakeForm() {
    const [cake, setCake] = useState({
        name: null,
        imageUrl: null,
        comment: null,
        yumFactor: null
    });
    const history = useHistory();
    const [{ data, loading, error }, executePost] = useAxios(
        {
            url: `${process.env.REACT_APP_API_URL}/cakes`,
            method: 'POST'
        },
        { manual: true }
    );
    
    const validationErrors = error && error.response.data
    
    const handleChange = (event) => {
        const {target} = event;
        const value = target.value;
        const {name} = target;
        setCake({
            ...cake,
            [name]: value
        });
    };
    
    const submit = () => {
        executePost({
            data: cake
        }).then(() => history.push('/'));
    };
    
    const hasError = (fieldName) => {
        return validationErrors && validationErrors.errors.some(x => x.field === fieldName);
    }
    
    const getErrorMessage = (fieldName) => {
        return validationErrors && validationErrors.errors.find(x => x.field === fieldName)?.message;
    }
    
    return (
        <>
            <h2>Add new cake</h2>
            <p>{validationErrors && validationErrors.message}</p>
            <Form>
                <FormGroup>
                    <Label for="name">Name</Label>
                    <Input type="text" name="name" id="name" placeholder="Enter a name" value={cake.name} invalid={hasError("name")} onChange={handleChange} />
                    <FormFeedback>{getErrorMessage("name")}</FormFeedback>
                </FormGroup>
                <FormGroup>
                    <Label for="imageUrl">Image URL</Label>
                    <Input type="text" name="imageUrl" id="imageUrl" placeholder="Enter an image url" value={cake.imageUrl} invalid={hasError("imageUrl")} onChange={handleChange} />
                    <FormFeedback>{getErrorMessage("imageUrl")}</FormFeedback>
                </FormGroup>
                <FormGroup>
                    <Label for="comment">Comment</Label>
                    <Input type="textarea" name="comment" id="comment" placeholder="Enter a comment" value={cake.comment} invalid={hasError("comment")} onChange={handleChange} />
                    <FormFeedback>{getErrorMessage("comment")}</FormFeedback>
                </FormGroup>
                <FormGroup>
                    <Label for="yumFactor">Yum Factor</Label>
                    <Input type="number" min="1" max="5" name="yumFactor" id="yumFactor" placeholder="Enter a yum factor between 1 and 5" value={cake.yumFactor} invalid={hasError("yumFactor")} onChange={handleChange} />
                    <FormFeedback>{getErrorMessage("yumFactor")}</FormFeedback>
                </FormGroup>
                <Button onClick={submit}>Submit</Button>
            </Form>
        </>

    );
}