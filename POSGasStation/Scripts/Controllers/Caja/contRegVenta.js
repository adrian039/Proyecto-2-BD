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
      this.prodList.push(data);
    });
  };

  $scope.generic=function(){
    alert("Using generic user");
    console.log("Current: "+clientService.getName());
    clientService.setDefaultClient();
    console.log("Current: "+clientService.getName());
    
  }
  
  $scope.registerSale=function(){

  };

}]);