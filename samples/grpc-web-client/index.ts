import * as grpcWeb from 'grpc-web';
import {GreeterClient} from './generated/AuthserviceServiceClientPb';
import {HelloReply, HelloRequest} from'./generated/authservice_pb';

const client = new GreeterClient('http://localhost:5000');

function callGrpcService(){
    console.log("callGrpcService")
    const request = new HelloRequest();
    request.setName('Hello World!');
    
    const metadata = {'custom-header-1': 'value1'};
    
    const call =client.sayHello(request, metadata, (err: grpcWeb.RpcError,
       response: HelloReply) => {
      if(err){
        console.error(err);
        return;
      }
      var message = response.getMessage();
      console.log(message);
      const ele = document.getElementById("message");
      if(ele) ele.innerHTML = "Response: " + message;
    });
    
    call.on('status', (status:grpcWeb.Status) => {
        console.log("Status: ", status)
    });
}

document.addEventListener("DOMContentLoaded", () => {
  const btn = document.getElementById('callGrpc')!;
  
  btn.addEventListener('click', () => {
    callGrpcService(); // Calling the globally exposed function
  });
})