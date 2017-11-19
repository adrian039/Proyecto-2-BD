angular.module("mainModule").controller("contRegVenta", ["$scope","$http","clientService","userService",'$location',
function($scope,$http,clientService,userService,$location) {
  $scope.idClient = "";
  $scope.ean="";
  $scope.qty="";
  $scope.type="";
  $scope.prodList=[];
  $scope.horaEntrada;

  $scope.init = function(){
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1; //January is 0!
    
    var today = new Date();
    var day = today.getDate() + "";
    var month = (today.getMonth() + 1) + "";
    var year = today.getFullYear() + "";
    var hour = today.getHours() + "";
    var minutes = today.getMinutes() + "";
    var seconds = today.getSeconds() + "";
    
    day = checkZero(day);
    month = checkZero(month);
    year = checkZero(year);
    hour = checkZero(hour);
    mintues = checkZero(minutes);
    seconds = checkZero(seconds);

    function checkZero(data){
      if(data.length == 1){
        data = "0" + data;
      }
      return data;
    } 
    
    this.horaEntrada=hour + ":" + minutes + ":" + seconds;
    console.log("Hora Entrada: "+this.horaEntrada);

  };

  $scope.verifyUser=function(){
    if(userService.getState()){
      var url = "http://gsprest.azurewebsites.net/api/Clientes?cedula="+this.idClient;
      $scope.getHttp(url,(data)=>{
        if(data){
          alert("Client:" +data.nombre+" "+data.papellido);
          clientService.setName(data.nombre);
          clientService.setpAp(data.papellido);
          clientService.setsAp(data.sapellido);
          clientService.setID(data.cedula);
        }
        else{
          $('#myModal').modal({ show: false});
          $('#myModal').modal("show");
        }
      })
    }
    else{
      alert("Cashier is not opent");
    }
  };

  $scope.addProduct=function(){
    if(userService.getState()){
      var url="http://gsprest.azurewebsites.net/api/ProductosSucursal?ean="+this.ean+"&suc="+userService.getSucursal()+"&cant="+this.qty;
      var sendData = {
      };
      $scope.postHttp(url,sendData,(data)=>{
        if(data){
          data["qty"]=parseInt(this.qty);
          var productos={
            "nombre": data.nombre,
            "qty":parseInt(this.qty),
            "ean":this.ean
          };

          this.prodList.push(productos);
        }
        else{
          alert("The product does not exist or is not available");
        }
      });
    }
    else{
      alert("Cashier not open");
    }
  };

  $scope.generic=function(){
    alert("Using generic user");
    console.log("Current: "+clientService.getName());
    clientService.setDefaultClient();
    console.log("Current: "+clientService.getName());
    
  }
  $scope.deleteElement=function(prod){
    for(var i=0;i<this.prodList.length;i++){
      this.prodList.splice(i,1);
      i--;
    }
  }
  
  $scope.registerSale=function(){
    if(userService.getState()){
      var fecha = new Date();
      var day = fecha.getDate() + "";
      var month = (fecha.getMonth() + 1) + "";
      var year = fecha.getFullYear() + "";
      var hour = fecha.getHours() + "";
      var minutes = fecha.getMinutes() + "";
      var seconds = fecha.getSeconds() + "";
      
      day = checkZero(day);
      month = checkZero(month);
      year = checkZero(year);
      hour = checkZero(hour);
      mintues = checkZero(minutes);
      seconds = checkZero(seconds);
  
      function checkZero(data){
        if(data.length == 1){
          data = "0" + data;
        }
        return data;
      }
      fecha=(year + "-" + month + "-" + day);
      var horaSalida=hour + ":" + minutes + ":" + seconds;
      var url="http://gsprest.azurewebsites.net/api/Ventas";
      var prodVenta=[];
      for (var i=0;i<this.prodList.length;i++){
        var productos={
          "ean":parseInt(this.prodList[i].ean),
          "cantidad":this.prodList[i].qty
        };
        prodVenta.push(productos);
      }
      var payType=this.type.split(":",1);
      var sendData = {
        "idCliente": clientService.getID(),
        "idEmpleado":userService.getUser().cedula,
        "idSucursal": parseInt(userService.getSucursal()),
        "tipoPago": parseInt(payType),
        "productos":prodVenta,
        "fecha":fecha,
        "starts":this.horaEntrada,
        "ends":horaSalida,
      };
      console.log(sendData);
      if(prodVenta.length>0){
        $scope.postHttp(url,sendData,(data)=>{
          console.log("Data: "+data);
          if(data!="null"){
            alert("Order completed");
          }
          else{
            alert("Error, check that the information is complete");
          }
        });
      }else{
        alert("You must add products");
      }
    }
    else{
      alert("Cashier not open");
    }

  };

}]);