using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using GraphicalEditor.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphicalEditor.Service
{
    public class MapObjectServices : IMapObjectServices
    {
        private IRepository _fileRepository;

        public MapObjectServices(IRepository fileRepository) {
            _fileRepository = fileRepository;
        }

        public void UpdateMapObject(MapObject mapObject)
        {
            _fileRepository.UpdateMapObject(mapObject);
        }

        public MapObject GetMapObjectById(long id)
        {
            foreach (MapObject mapObject in MainWindow._allMapObjects)
            {
                if (mapObject.MapObjectEntity.Id == id)
                {
                    return mapObject;
                }
            }

            return null;
        }

        public List<MapObject> SearchMapObjects(MapObjectType searchedMapObjectType)
        {
            List<MapObject> searchResultMapObjects = new List<MapObject>();

            foreach (MapObject mapObject in MainWindow._allMapObjects)
            {
                if (mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == searchedMapObjectType.TypeOfMapObject)
                {
                    searchResultMapObjects.Add(mapObject);
                }
            }
            return searchResultMapObjects;
        }

        public List<MapObject> GetNeighboringRoomsForRoom(MapObject room)
        {
            List<MapObject> neighboringRooms = new List<MapObject>();

            foreach (MapObject mapObject in MainWindow._allMapObjects)
            {
                if (mapObject.CheckIsRoom() && mapObject.MapObjectEntity.Id != room.MapObjectEntity.Id)
                {
                    if ((((Room)mapObject.MapObjectEntity).BuildingId == ((Room)room.MapObjectEntity).BuildingId)
                        && (((Room)mapObject.MapObjectEntity).Floor == ((Room)room.MapObjectEntity).Floor))
                    {
                        if (Canvas.GetLeft(mapObject.Rectangle) == Canvas.GetLeft(room.Rectangle) || Canvas.GetTop(mapObject.Rectangle) == Canvas.GetTop(room.Rectangle))
                        {
                            neighboringRooms.Add(mapObject);
                        }
                    }
                }
            }
            return neighboringRooms;
        }
    }
}
