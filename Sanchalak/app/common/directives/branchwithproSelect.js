app.directive("branchwithproSelect", function () {
    return {
        templateUrl: "UI/components/branchwithproSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammeBranchListGetByProgrammeIdList = [];
            $scope.defaultBranchesList = [];
            var branchesObj = {
                Id : 0,
                BranchName: "Select Branch"
            }
            $scope.defaultBranchesList.push(branchesObj);

            $scope.getMstProgrammeBranchListGetByProgrammeIdList = function (data) {

                $rootScope.showLoading = true;

                $http({
                    method: 'POST',
                    url: 'api/MstProgramInstance/MstProgrammeBranchListGetByProgrammeId',
                    data: { ProgrammeId  : data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code != "200") {
                      
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.MstProgrammeBranchListGetByProgrammeIdList = $scope.defaultBranchesList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
            }
     /*       $scope.MstProgrammeMasterGet();*/
        }],
        link: function (e, t, a) {
        }
    }
})