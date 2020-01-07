// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaytracingPlayground
{
    public class World
    {
        private HittableList items = new HittableList();

        public World()
        {
        }

        public HittableList Items => this.items;

        public void Update()
        {
        }

        public void Draw()
        {
        }

        public void Add(IHittable hittable)
        {
            if (hittable != null)
            {
                this.Items.Hittables.Add(hittable);
            }
        }
    }
}
