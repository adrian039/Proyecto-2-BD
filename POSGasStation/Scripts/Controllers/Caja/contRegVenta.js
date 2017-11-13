angular.module("mainModule").controller("contRegVenta", ["$scope","$http","clientService","userService",'$location',
function($scope,$http,clientService,userService,$location) {
  $scope.idClient = "";
  $scope.ean="";
  $scope.qty="";
  $scope.prodList=[];

  $scope.init = function(){
  };

  $scope.verifyUser=function(){
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
  };

  $scope.addProduct=function(){

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
          "qty":parseInt(this.qty)
        };

        this.prodList.push(productos);
      }
      else{
        alert("The product does not exist or is not available");
      }
    });
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

  };

}]);