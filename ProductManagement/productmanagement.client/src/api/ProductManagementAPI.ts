import apiClient from './apiClient';
import { Product } from '../model/product';

class ProductManagementAPI {
    public async getAllProducts(): Promise<Product[]> {
        try {
            const response = await apiClient.get('/products');
            return response.data;
        } catch (error) {           
            throw new Error('Error retrieving products in ProductManagementAPI: ' + error);
        }
    }
}

export default ProductManagementAPI