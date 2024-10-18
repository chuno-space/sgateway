
import {GreeterClient} from '/generated/authservice_grpc_web_pb.js';
import {HelloRequest} from'/generated/authservice_pb.js';

const client = new GreeterClient('http://localhost:5000');

function callGrpcService(){
    console.log("callGrpcService")
    const request = new HelloRequest();
    request.setName('Hello World!');
    
    const metadata = {'custom-header-1': 'value1'};
    
    const call =client.sayHello(request, metadata, (err, response) => {
      if(err){
        console.error(err);
        return;
      }
      console.log(response.getMessage());
    });
    
    call.on('status', (status) => {
        console.log("Status: ", status)
    });
}

window.callGrpcService = callGrpcService;