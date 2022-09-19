app.controller('DeanSignCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, Upload) {

    var favoriteCookie = $cookies.get('token');    
    if (!favoriteCookie) {
        $state.go('login');
    }    
    $scope.signData = {};

    $scope.DeanSignGet = function () {
        
        $http({
            method: 'GET',
            url: 'api/DeanSign/DeanSignGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                              
                if (response.response_code == "0") {

                    alert(response.obj);
                    $state.go('login');
                }
                else if (response.response_code == "201") {

                    alert(response.obj);
                }
                else {

                    $scope.signData = response.obj;
                    $scope.FileNameFinal = $scope.signData.DSign;
					$scope.imgReload = 	'?decache=' + Math.random();
                    //$scope.btn_ds = true;
                    debugger
                }
            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.DeanSignUpdate = function () {
        debugger
        if ($scope.signData.NameOfAuthority == "" || $scope.signData.NameOfAuthority == null ||
            $scope.signData.NameOfAuthority == undefined) {

            alert("Please enter your Name");
        }
        else if ($scope.FileNameFinal == "" || $scope.FileNameFinal == null || $scope.FileNameFinal == undefined) {

            alert("Please uplaod your Signature");
        }
        else {

            $scope.signData.DSign = $scope.FileNameFinal;

            $http({
                method: 'Post',
                url: 'api/DeanSign/DeanSignUpdate',
                data: $scope.signData,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    debugger
                    if (response.response_code == "0") {

                        alert(response.obj);
                        $state.go('login');
                    }
                    else if (response.response_code == "201") {

                        alert(response.obj);
                    }
                    else {
                        debugger
                        alert(response.obj);
                        //$scope.signData = {};
                        //$scope.FileNameFinal = null;
                        $("#upSign").val("");
                        $scope.DeanSignGet();
                    }
                })
                .error(function (res) {
                    alert(res);
                });
        }        
    };
    //Genric Config for file size
    $scope.byId = function (data) {

        return $http({
            method: 'POST',
            url: 'api/DeanSign/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };
    $scope.UploadFile_Sign = function ($files) {
        debugger
        if ($scope.signData.NameOfAuthority == "" || $scope.signData.NameOfAuthority == null ||
            $scope.signData.NameOfAuthority == undefined) {

            alert("Please enter your Name");
            $("#upSign").val("");
            return false;
        }
        $scope.SelectedFiles = $files;
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        var FileName = "DeanSign";
        $scope.FileName = FileName + fileExtn;
        //alert($scope.FileNameOfLastName);
        $cookies.put("dsFileName", $scope.FileName);
        //$cookies.put("req", req);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var txtpc = document.getElementById("upSign").value;
            var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
            const fi = document.getElementById("upSign");

            if (!allowedExtensions.exec(txtpc)) {

                txtpc.value = '';
                $scope.FileNameFinal = undefined;
                $("#upSign").val("");

                alert("It only accepts jpg or jpeg or png file");                
                return false;
            }
            else if (fi.files.length > 0) {

                debugger
                $scope.byId(6).then(function (response) {

                    $scope.maxVal = response.data.obj[0].Value;
                    debugger
                    const fsize = fi.files.item(0).size;
                    const file = Math.round((fsize / 1024));
                    if (file >= $scope.maxVal) {

                        $scope.FileNameFinal = undefined;
                        $("#upSign").val("");

                        alert("Please select a file below 50 KB");
                        return false;
                    }
                    else {
                        debugger
                        Upload.upload({

                            url: 'api/DeanSign/UploadFile_Sign',
                            data: {
                                files: $scope.SelectedFiles
                            }
                        }).then(function (response) {
                            debugger
                            if (response.data.response_code == "0") {

                                alert(response.data.obj);
                                $state.go('login');
                            }
                            else if (response.data.response_code == "200") {

                                debugger                                
                                //$scope.btn_ds = false;
                                $scope.resToken = response.data.obj;
                                $scope.FileNameFinal = $scope.resToken + "_" + $scope.FileName;
                                //$scope.Result = response.data;
                                alert("File Uploaded Successfully");
                                $scope.DeanSignUpdate();
                            }
                            else {

                                $scope.FileNameFinal = undefined;
                                $("#upSign").val("");
                                alert("File Upload Failed!");
                                return false;
                            }

                        }, function (response) {

                            if (response.status > 0) {

                                var errorMsg = response.status + ': ' + response.data;
                            }
                        }, function (evt) {

                            var element = angular.element(document.querySelector('#dvProgress'));
                            $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                            element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                        });
                    }
                }).catch(function (res) {

                    alert(res.obj);
                });
            }
        }
    };
});



