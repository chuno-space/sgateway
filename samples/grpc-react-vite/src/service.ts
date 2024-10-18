//import * as grpcWeb from 'grpc-web';

import { GreeterClient } from 'grpc-web-client-gen/AuthserviceServiceClientPb';
import { HelloRequest } from 'grpc-web-client-gen/authservice_pb';

const client = new GreeterClient('http://localhost:5000');

export async function callGrpcService(){
    console.log("callGrpcService")
    const request = new HelloRequest();
    request.setName('Hello World!');
    
    const metadata = {'custom-header-1': 'value1'};
    
    
    const response  = await client.sayHello(request, metadata)
    
   return response.getMessage();
}