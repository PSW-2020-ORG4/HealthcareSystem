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
using GraphicalEditor.Constants;
using System.Windows;

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
                        if (CheckIfMapObjectsOverlap(room,mapObject))
                        {
                            neighboringRooms.Add(mapObject);
                        }
                    }
                }
            }
            return neighboringRooms;
        }

        private bool CheckIfMapObjectsOverlap(MapObject firstMapObject, MapObject secondMapObject)
        {
           for(double coordinateX = Canvas.GetLeft(firstMapObject.Rectangle); coordinateX <= (Canvas.GetLeft(firstMapObject.Rectangle) + firstMapObject.Rectangle.Width); coordinateX += firstMapObject.Rectangle.Width/5)
            {
                if (CheckIfPointInsideMapObject(new Point(coordinateX, Canvas.GetTop(firstMapObject.Rectangle)) , secondMapObject))
                {
                    return true;
                }
            }

            for (double coordinateX = Canvas.GetLeft(firstMapObject.Rectangle); coordinateX <= (Canvas.GetLeft(firstMapObject.Rectangle) + firstMapObject.Rectangle.Width); coordinateX += firstMapObject.Rectangle.Width / 5)
            {
                if (CheckIfPointInsideMapObject(new Point(coordinateX, Canvas.GetTop(firstMapObject.Rectangle) + firstMapObject.Rectangle.Height), secondMapObject))
                {
                    return true;
                }
            }

            for (double coordinateY = Canvas.GetTop(firstMapObject.Rectangle); coordinateY <= (Canvas.GetTop(firstMapObject.Rectangle) + firstMapObject.Rectangle.Height); coordinateY += firstMapObject.Rectangle.Height / 5)
            {
                if (CheckIfPointInsideMapObject(new Point(Canvas.GetLeft(firstMapObject.Rectangle), coordinateY), secondMapObject))
                {
                    return true;
                }
            }

            for (double coordinateY = Canvas.GetTop(firstMapObject.Rectangle); coordinateY <= (Canvas.GetTop(firstMapObject.Rectangle) + firstMapObject.Rectangle.Height); coordinateY += firstMapObject.Rectangle.Height / 5)
            {
                if (CheckIfPointInsideMapObject(new Point(Canvas.GetLeft(firstMapObject.Rectangle) + firstMapObject.Rectangle.Width, coordinateY), secondMapObject))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfPointInsideMapObject(Point point, MapObject mapObject)
        {
            if ((point.X > Canvas.GetLeft(mapObject.Rectangle) && point.X < Canvas.GetLeft(mapObject.Rectangle) + mapObject.Rectangle.Width) 
                && ((point.Y > Canvas.GetTop(mapObject.Rectangle) && point.Y < Canvas.GetTop(mapObject.Rectangle) + mapObject.Rectangle.Height)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
