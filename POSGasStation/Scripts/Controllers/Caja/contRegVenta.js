angular.module("mainModule").controller("contRegVenta", ["$scope","$http","clientService","userService",'$location',
function($scope,$http,clientService,userService,$location) {
  $scope.idClient = "";
  $scope.ean="";
  $scope.qty="";
  $scope.type="";
  $scope.prodList=[];

  $scope.init = function(){
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
      var url="http://gsprest.azurewebsites.net/api/ProductosSucursal";
      var sendData = {
        "idproducto": parseInt(this.ean),
        "cantidad": parseInt(this.qty),
        "idsucursal": parseInt(userService.getSucursal())
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
        "productos":prodVenta
      };
      console.log(prodVenta);
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