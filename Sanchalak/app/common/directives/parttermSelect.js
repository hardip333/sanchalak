app.directive("parttermSelect", function () {
    return {
        templateUrl: "UI/components/parttermSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammePartTermGetByProgrammeIdList = [];
            $scope.defaultPartTermsList = [];
            var parttermsObj = {
                Id : 0,
                PartTermName: "Select Programme Part Term"
            }
            $scope.defaultPartTermsList.push(parttermsObj);

            $scope.MstProgrammePartTermGetByProgrammeId = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/MstProgrammePartTerm/MstProgrammePartTermGetByProgrammeId',
                    data: { ProgrammeId:data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.MstProgrammePartTermGetByProgrammeIdList = $scope.defaultPartTermsList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
   /*         $scope.MstProgrammePartTermGetByProgrammeId();*/


        }],
        link: function (e, t, a) {
        }
    }
})