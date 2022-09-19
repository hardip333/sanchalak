app.directive("parttermbranchprogrammeSelect", function () {
    return {
        templateUrl: "UI/components/parttermbranchprogrammeSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammePartTermGetByProgrammeIdAndBranchIdList = [];
            $scope.defaultParttermbranchproList = [];
            var propartObj = {
                Id : 0,
                PartTermName: "Select Programme Part Term"
            }
            $scope.defaultParttermbranchproList.push(propartObj);

            $scope.getMstProgrammePartTermGetByProgrammeIdAndBranchIdList = function (data) {
                //alert(1);
                $rootScope.showLoading = true;
                if (data.ProgrammeId !== null && data.BranchId !== null) {
                
                $http({
                    method: 'POST',
                    url: 'api/MstProgrammePartTerm/MstProgrammePartTermGetByProgrammeIdAndBranchId',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code != "200") {
                      
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.MstProgrammePartTermGetByProgrammeIdAndBranchIdList = $scope.defaultParttermbranchproList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
                }
            }
     /*       $scope.MstProgrammeMasterGet();*/
        }],
        link: function (e, t, a) {
        }
    }
})