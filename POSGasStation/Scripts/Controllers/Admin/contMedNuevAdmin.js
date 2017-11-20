
angular.module("mainModule").controller("contMedNuevAdmin", ["$scope","$http",'$location',"userService",
function($scope,$http,$location,userService) {

  $scope.ean;
  $scope.idProveedor;
  $scope.nombre;
  $scope.descripcion;


  $scope.prodList;
  $scope.sucList;
  $scope.idSuc;
  $scope.eanCode;
  $scope.precio;
  $scope.cantidad;


  $scope.providerList;

      $scope.init = function(){
        var url = "http://gsprest.azurewebsites.net/api/Proveedores";
        $scope.getHttp(url,(data)=>{
          this.providerList=data;
        });

        url = "http://gsprest.azurewebsites.net/api/Productos";
        $scope.getHttp(url,(data)=>{
          this.prodList=data;
        });

        url='http://gsprest.azurewebsites.net/api/Sucursales?idEmpresa='+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.sucList=data;
        });


      }

      $scope.addProduct=function(){
        var url = "http://gsprest.azurewebsites.net/api/ProductosSucursal";
        var sendData = {
          "idsucursal": parseInt(this.idSuc.split(":",1)),
          "idproducto": parseInt(this.eanCode.split(":",1)),
          "cantidad": parseInt(this.cantidad),
          "precio":parseInt(this.precio),
          "estado":1
        };
        
       $scope.postHttp(url,sendData,(data)=>{
          console.log("Data: "+data);
          alert("Process Completed");
        });

      };


      $scope.createProduct=function(){
        var url = "http://gsprest.azurewebsites.net/api/Productos";
        var sendData = {
          "ean": parseInt(this.ean),
          "nombre": this.nombre,
          "descripcion": this.descripcion,
          "imagen":globalImage,
          "idproveedor":parseInt(this.idProveedor.split(":",1)),
          "estado":1
        };

       $scope.postHttp(url,sendData,(data)=>{
          console.log("Data: "+data);
          alert("Product Created");
          $scope.init();
        });

        
      };

    }]);
